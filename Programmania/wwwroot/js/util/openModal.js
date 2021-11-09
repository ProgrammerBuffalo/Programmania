function initModal(modalEl, modalEnterEl, modalExitEl) {
    modalEnterEl.onclick = function () {
        modalEl.style.display = 'block';
    }

    modalExitEl.onclick = function () {
        modalEl.style.display = 'none';
    }

    window.addEventListener('click', function () {
        if (event.target == modalEl)
            modalEl.style.display = 'none';
    });
}