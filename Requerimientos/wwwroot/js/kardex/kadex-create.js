// wwwroot/js/kardex/kardex-create.js
(function () {
    "use strict";

    const cantidad = document.getElementById("Cantidad");
    const precioUnitario = document.getElementById("PrecioUnitario");
    const precioVentaUI = document.getElementById("PrecioVentaUI");

    if (!cantidad || !precioUnitario || !precioVentaUI) return;

    const toNumber = (value) => {
        if (value === null || value === undefined) return 0;
        // acepta coma o punto
        const normalized = value.toString().replace(",", ".");
        const n = parseFloat(normalized);
        return Number.isFinite(n) ? n : 0;
    };

    const format2 = (n) => {
        // 2 decimales fijo para money
        return (Math.round(n * 100) / 100).toFixed(2);
    };

    const recalcular = () => {
        const c = toNumber(cantidad.value);
        const pu = toNumber(precioUnitario.value);
        const pv = c * pu;
        precioVentaUI.value = format2(pv);
    };

    // recalcula en tiempo real
    ["input", "change"].forEach(evt => {
        cantidad.addEventListener(evt, recalcular);
        precioUnitario.addEventListener(evt, recalcular);
    });

    // inicial
    recalcular();
})();
