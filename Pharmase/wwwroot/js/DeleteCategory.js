$(document).ready(function () {
    // Bind click event to elements with the class .js-delete
    $('.js-delete').on('click', function () {
        var btn = $(this); // The clicked button
        var itemType = 'Category'; // Always assume the item is a Category
        var deleteUrl = `/Category/DeleteCategory/${btn.data('id')}`; // Always use the Category delete URL

        // Configure SweetAlert2
        const swal = Swal.mixin({
            customClass: {
                confirmButton: 'btn btn-danger mx-2', // Style for confirm button
                cancelButton: 'btn btn-light' // Style for cancel button
            },
            buttonsStyling: false // Disable default button styling
        });

        // Show confirmation dialog
        swal.fire({
            title: `Are you sure that you want to delete this ${itemType}?`,
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            cancelButtonText: 'No, cancel!',
            reverseButtons: true // Reverse button order (confirm on the right)
        }).then((result) => {
            if (result.isConfirmed) {
                // If user confirms, send AJAX request
                $.ajax({
                    url: deleteUrl,
                    method: 'DELETE',
                    success: function () {
                        // On success, show success message and reload page
                        swal.fire(
                            'Deleted!',
                            `${itemType} has been deleted.`,
                            'success'
                        ).then(() => {
                            location.reload();
                        });
                    },
                    error: function () {
                        // On error, show error message
                        swal.fire(
                            'Oops...',
                            'Something went wrong.',
                            'error'
                        );
                    }
                });
            }
        });
    });
});