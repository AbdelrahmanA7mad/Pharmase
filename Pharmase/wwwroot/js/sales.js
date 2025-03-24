document.addEventListener('DOMContentLoaded', function () {
    const medicines = [];
    const apiUrl = document.getElementById('apiUrls').dataset.getMedicines;
    const medicineSearch = document.getElementById('medicineSearch');
    const suggestedMedicines = document.getElementById('suggestedMedicines');
    const saleItemsList = document.getElementById('saleItemsList');
    const saleForm = document.getElementById('saleForm');

    // Load Medicines
    fetch(apiUrl)
        .then(response => response.json())
        .then(data => medicines.push(...data))
        .catch(error => console.error('Error loading medicines:', error));

    // Medicine Search
    medicineSearch.addEventListener('input', function (e) {
        const searchTerm = e.target.value.toLowerCase();
        const filteredMedicines = medicines.filter(medicine =>
            medicine.name.toLowerCase().includes(searchTerm)
        );

        suggestedMedicines.innerHTML = filteredMedicines.length > 0 ?
            filteredMedicines.map(medicine => `
                <a href="#" class="list-group-item suggested-medicine-item" 
                   data-id="${medicine.id}" 
                   data-name="${medicine.name}" 
                   data-price="${medicine.unitPrice}">
                    ${medicine.name}
                </a>`
            ).join('') :
            '<p class="text-muted">No matching medicines found</p>';
    });

    // Add Medicine to List
    document.addEventListener('click', function (e) {
        if (e.target.classList.contains('suggested-medicine-item')) {
            const medicine = e.target.dataset;
            if (document.querySelector(`tr[data-id="${medicine.id}"]`)) {
                alert('This medicine has already been added');
                return;
            }

            const index = saleItemsList.children.length;
            const row = document.createElement('tr');
            row.dataset.id = medicine.id;
            row.innerHTML = `
                <td data-label="Medicine Name">
                    ${medicine.name}
                    <input type="hidden" name="SaleItems[${index}].MedicineId" value="${medicine.id}" />
                </td>
                <td data-label="Quantity">
                    <input type="number" 
                           class="form-control quantity-input" 
                           name="SaleItems[${index}].QuantitySold" 
                           min="1" 
                           value="1" 
                           required />
                </td>
                <td data-label="Price" class="price">${medicine.price}</td>
                <td>
                    <button type="button" class="delete-btn">
                        <i class="fas fa-trash-alt"></i> Delete
                    </button>
                </td>
            `;

            saleItemsList.appendChild(row);
            medicineSearch.value = '';
            suggestedMedicines.innerHTML = '';
            calculateTotalPrice();
        }
    });

    // Delete Item
    document.addEventListener('click', function (e) {
        if (e.target.closest('.delete-btn')) {
            const row = e.target.closest('tr');
            row.remove();
            reindexRows();
            calculateTotalPrice();
        }
    });

    // Quantity Input Handler
    document.addEventListener('input', function (e) {
        if (e.target.classList.contains('quantity-input')) {
            calculateTotalPrice();
        }
    });

    // Form Submission
    saleForm.addEventListener('submit', function (e) {
        e.preventDefault();
        const quantities = [...document.querySelectorAll('.quantity-input')];
        const isValid = quantities.every(input => input.value > 0);

        if (!isValid) {
            alert('Quantity must be greater than 0 for all items');
            return;
        }

        showConfirmationModal().then(confirmed => {
            if (confirmed) {
                const totalPrice = document.getElementById('totalPrice').textContent;
                const hiddenInput = document.createElement('input');
                hiddenInput.type = 'hidden';
                hiddenInput.name = 'TotalPrice';
                hiddenInput.value = totalPrice;
                saleForm.appendChild(hiddenInput);
                saleForm.submit();
            }
        });
    });

    // Calculate Total Price
    function calculateTotalPrice() {
        let total = 0;
        document.querySelectorAll('#saleItemsList tr').forEach(row => {
            const price = parseFloat(row.querySelector('.price').textContent);
            const quantity = parseInt(row.querySelector('.quantity-input').value);
            total += price * quantity;
        });
        document.getElementById('totalPrice').textContent = total.toFixed(2);
    }

    // Reindex Rows
    function reindexRows() {
        const rows = document.querySelectorAll('#saleItemsList tr');
        rows.forEach((row, index) => {
            row.querySelector('input[name$="MedicineId"]').name = `SaleItems[${index}].MedicineId`;
            row.querySelector('input[name$="QuantitySold"]').name = `SaleItems[${index}].QuantitySold`;
        });
    }

    // Confirmation Modal
    function showConfirmationModal() {
        return new Promise((resolve) => {
            const modal = document.getElementById('confirmationModal');
            const confirmBtn = modal.querySelector('.confirm-btn');
            const cancelBtn = modal.querySelector('.cancel-btn');

            modal.classList.add('show');

            const cleanup = () => {
                modal.classList.remove('show');
                confirmBtn.removeEventListener('click', confirmHandler);
                cancelBtn.removeEventListener('click', cancelHandler);
            };

            const confirmHandler = () => {
                cleanup();
                resolve(true);
            };

            const cancelHandler = () => {
                cleanup();
                resolve(false);
            };

            confirmBtn.addEventListener('click', confirmHandler);
            cancelBtn.addEventListener('click', cancelHandler);
        });
    }
});