document.addEventListener('DOMContentLoaded', function () {
    document.querySelectorAll('form').forEach(function (form) {
        const textarea = form.querySelector('textarea#texto, textarea#Texto');
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

            const opciones = form.querySelectorAll('input[name="OpcionTransformar"]');
            const opcionesDiv = form.querySelector('.horizontal');
            const opcionSeleccionada = Array.from(opciones).some(opt => opt.checked);
            if (opciones.length && !opcionSeleccionada) {
                const error = document.createElement('div');
                error.className = 'alert alert-danger mt-2';
                error.textContent = 'Debe seleccionar entre formal e informal.';
                opcionesDiv.parentNode.insertBefore(error, opcionesDiv.nextSibling);
                valid = false;
            }

            if (!valid) e.preventDefault();
        });
    });
});
