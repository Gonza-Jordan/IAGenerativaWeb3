document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('form').forEach(function (form) {
        const textarea = form.querySelector('textarea#texto');
        if (!textarea) return; 

        form.addEventListener('submit', function (e) {
            form.querySelectorAll('.alert').forEach(el => el.remove());

            const value = textarea.value.trim();
            let valid = true;

            if (value === "") {
                const error = document.createElement('div');
                error.className = 'alert alert-danger';
                error.textContent = 'Debe ingresar un texto.';
                textarea.parentNode.insertBefore(error, textarea.nextSibling);
                valid = false;
            } else if (/^\d+$/.test(value)) {
                const error = document.createElement('div');
                error.className = 'alert alert-danger';
                error.textContent = 'Solo ingresó números, debe ingresar texto.';
                textarea.parentNode.insertBefore(error, textarea.nextSibling);
                valid = false;
            }

            if (!valid) e.preventDefault();
        });
    });
});
