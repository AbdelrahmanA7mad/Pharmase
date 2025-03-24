let currentLanguage = localStorage.getItem('language') || 'en';

// Mobile menu functionality
function toggleMobileMenu() {
    const navLinks = document.querySelector('.nav-links');
    navLinks.classList.toggle('show');
}

function toggleLanguage() {
    currentLanguage = currentLanguage === 'en' ? 'ar' : 'en';
    localStorage.setItem('language', currentLanguage); // Store language preference
    document.documentElement.lang = currentLanguage;
    document.documentElement.dir = currentLanguage === 'ar' ? 'rtl' : 'ltr';
    document.querySelector('.current-lang').textContent = currentLanguage.toUpperCase();
    updateContent();
}

function updateContent() {
    const elements = document.querySelectorAll('[data-translate]');
    elements.forEach(element => {
        const key = element.getAttribute('data-translate');
        if (translations[currentLanguage] && translations[currentLanguage][key]) {
            element.textContent = translations[currentLanguage][key];
        }
    });
}

// Close mobile menu when clicking outside
document.addEventListener('click', (e) => {
    const navLinks = document.querySelector('.nav-links');
    const mobileMenuBtn = document.querySelector('.mobile-menu-btn');

    if (!navLinks.contains(e.target) && !mobileMenuBtn.contains(e.target)) {
        navLinks.classList.remove('show');
    }
});

// Handle window resize
window.addEventListener('resize', () => {
    if (window.innerWidth > 768) {
        document.querySelector('.nav-links').classList.remove('show');
    }
});

// Initialize the page with stored language preference
document.addEventListener('DOMContentLoaded', () => {
    document.documentElement.lang = currentLanguage;
    document.documentElement.dir = currentLanguage === 'ar' ? 'rtl' : 'ltr';
    document.querySelector('.current-lang').textContent = currentLanguage.toUpperCase();
    updateContent();
});
