(function () {
    const selCategoria = document.getElementById("CategoriaId");
    const inpNumero = document.getElementById("CodigoNumero");
    const hidCodigo = document.getElementById("Codigo");

    const spnPrefijo = document.getElementById("spnPrefijo");
    const spnPrefijoInput = document.getElementById("spnPrefijoInput");
    const spnCodigoFinal = document.getElementById("spnCodigoFinal");

    const inpUnidad = document.getElementById("Unidad");

    function getPrefijoSeleccionado() {
        if (!selCategoria) return "";
        const opt = selCategoria.options[selCategoria.selectedIndex];
        if (!opt) return "";
        const pref = opt.getAttribute("data-prefijo");
        return pref ? pref.trim().toUpperCase() : "";
    }

    function renderPrefijo(prefijo) {
        const p = prefijo ? (prefijo + "-") : "-";
        if (spnPrefijo) spnPrefijo.textContent = p;
        if (spnPrefijoInput) spnPrefijoInput.textContent = p;
    }

    function renderCodigoFinal(prefijo) {
        const num = (inpNumero && inpNumero.value) ? inpNumero.value.trim() : "";
        const codigo = (prefijo && num) ? `${prefijo}-${num}` : "";
        if (spnCodigoFinal) spnCodigoFinal.textContent = codigo || "-";
        if (hidCodigo) hidCodigo.value = codigo;
    }

    function onCategoriaChange() {
        const pref = getPrefijoSeleccionado();
        renderPrefijo(pref);
        renderCodigoFinal(pref);
    }

    function onNumeroChange() {
        const pref = getPrefijoSeleccionado();
        renderCodigoFinal(pref);
    }

    function onUnidadInput() {
        if (!inpUnidad) return;
        inpUnidad.value = (inpUnidad.value || "").toUpperCase().replace(/\s+/g, "");
    }

    // ✅ Prellenar el número desde el código actual (COM-12 -> 12)
    function preloadNumeroFromCodigo() {
        if (!hidCodigo || !inpNumero) return;
        const codigo = (hidCodigo.value || "").trim().toUpperCase();
        if (!codigo) return;

        const parts = codigo.split("-");
        if (parts.length >= 2) {
            const num = parts[parts.length - 1]; // soporta prefijos con guiones si algún día existen
            if (num && !inpNumero.value) inpNumero.value = num;
        }
    }

    // Eventos
    if (selCategoria) selCategoria.addEventListener("change", onCategoriaChange);
    if (inpNumero) inpNumero.addEventListener("input", onNumeroChange);
    if (inpUnidad) inpUnidad.addEventListener("input", onUnidadInput);

    // Init
    preloadNumeroFromCodigo();
    onCategoriaChange();
})();
