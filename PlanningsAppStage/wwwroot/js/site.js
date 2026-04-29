// =====================================================
// UNIVERSEEL TABEL SORTEREN & FILTEREN
// Werkt met OF zonder <tbody>
// Geen layout shifts
// =====================================================

document.addEventListener("DOMContentLoaded", () => {

    // ===============================
    // SORTEREN PER KOLOM
    // ===============================
    document.querySelectorAll(".sort-btn").forEach(btn => {
        btn.addEventListener("click", () => {

            const th = btn.closest("th");
            const table = th.closest("table");

            // ✅ tbody indien aanwezig, anders table
            const body = table.tBodies.length > 0 ? table.tBodies[0] : table;

            const columnIndex = Array.from(th.parentNode.children).indexOf(th);

            // Alle rijen behalve de header
            const rows = Array.from(body.querySelectorAll("tr"))
                .filter(r => r.parentElement === body);

            const asc = btn.innerText === "▲";
            btn.innerText = asc ? "▼" : "▲";

            rows.sort((a, b) => {
                let A = a.cells[columnIndex].innerText.trim();
                let B = b.cells[columnIndex].innerText.trim();

                // Datum sortering
                if (!isNaN(Date.parse(A)) && !isNaN(Date.parse(B))) {
                    return asc
                        ? new Date(A) - new Date(B)
                        : new Date(B) - new Date(A);
                }

                // Tekst / nummer sortering
                return asc
                    ? A.localeCompare(B, "nl", { numeric: true })
                    : B.localeCompare(A, "nl", { numeric: true });
            });

            rows.forEach(r => body.appendChild(r));
        });
    });

    // ===============================
    // FILTEREN PER KOLOM
    // ===============================
    document.querySelectorAll(".filter-btn").forEach(btn => {
        btn.addEventListener("click", () => {

            const th = btn.closest("th");
            const table = th.closest("table");
            const body = table.tBodies.length > 0 ? table.tBodies[0] : table;

            const columnIndex = Array.from(th.parentNode.children).indexOf(th);
            const filter = prompt("Filter op:");

            if (filter === null) return;

            body.querySelectorAll("tr").forEach(row => {

                const text = row.cells[columnIndex].innerText.toLowerCase();
                const value = filter.toLowerCase();

                row.style.display = text.includes(value) ? "" : "none";

            });
        });
    });

});