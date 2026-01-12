(function () {
    const simboloInput = document.getElementById("Simbolo");

    if (!simboloInput) return;

    simboloInput.addEventListener("input", function () {
        this.value = (this.value || "")
            .toUpperCase()
            .replace(/\s+/g, "");
    });
})();