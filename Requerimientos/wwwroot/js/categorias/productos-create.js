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
        // Intento 1: data-prefijo (recomendado)
        const pref = opt.getAttribute("data-prefijo");
        if (pref && pref.trim()) return pref.trim().toUpperCase();
        // Fallback: si el texto viene como "COMBUSTIBLE (COM)"
        // NO es ideal, pero cubre casos si no tienes data-prefijo.
        const txt = (opt.text || "").trim();
        const m = txt.match(/\(([^)]+)\)\s*$/);
        return m ? (m[1] || "").trim().toUpperCase() : "";
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

    // Normaliza unidad a mayúsculas sin espacios
    function onUnidadInput() {
        if (!inpUnidad) return;
        inpUnidad.value = (inpUnidad.value || "").toUpperCase().replace(/\s+/g, "");
    }

    // Inicializar
    if (selCategoria) selCategoria.addEventListener("change", onCategoriaChange);
    if (inpNumero) inpNumero.addEventListener("input", onNumeroChange);
    if (inpUnidad) inpUnidad.addEventListener("input", onUnidadInput);

    // Render inicial (por si el model trae valor o recarga por errores)
    onCategoriaChange();
})();
