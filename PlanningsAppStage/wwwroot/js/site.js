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

            const body = table.tBodies.length > 0 ? table.tBodies[0] : table;
            const columnIndex = Array.from(th.parentNode.children).indexOf(th);

            const rows = Array.from(body.querySelectorAll("tr"))
                .filter(r => r.parentElement === body);

            const asc = btn.innerText === "▲";
            btn.innerText = asc ? "▼" : "▲";

            rows.sort((a, b) => {
                let A = a.cells[columnIndex].innerText.trim();
                let B = b.cells[columnIndex].innerText.trim();

                if (!isNaN(Date.parse(A)) && !isNaN(Date.parse(B))) {
                    return asc
                        ? new Date(A) - new Date(B)
                        : new Date(B) - new Date(A);
                }

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
                row.style.display = text.includes(filter.toLowerCase()) ? "" : "none";
            });
        });
    });
});


// =====================================================
// FAKE DROPDOWN - SOORT (InfoLocaties & InfoWorkshops)
// =====================================================

document.addEventListener("click", function (e) {

    // -------------------------------
    // OPEN / SLUIT DROPDOWN
    // -------------------------------
    const select = e.target.closest(".soortSelect");
    if (select) {
        const wrapper = select.closest(".soort-wrapper");
        if (!wrapper) return;

        const dropdown = wrapper.querySelector(".soortDropdown");
        if (!dropdown) return;

        dropdown.classList.toggle("d-none");
        e.stopPropagation();
        return;
    }

    // -------------------------------
    // SELECTEER SOORT
    // -------------------------------
    const optie = e.target.closest(".soort-optie");
    if (optie && !e.target.classList.contains("verwijder-soort")) {

        const wrapper = optie.closest(".soort-wrapper");
        if (!wrapper) return;

        const value = optie.dataset.value;
        const label = wrapper.querySelector(".soortGeselecteerd");
        const hidden = wrapper.querySelector(".soortValue");
        const dropdown = wrapper.querySelector(".soortDropdown");

        if (!label || !hidden || !dropdown) return;

        label.textContent = value;
        hidden.value = value;
        dropdown.classList.add("d-none");
        return;
    }

    // -------------------------------
    // ❌ VERWIJDER SOORT (met confirm)
    // -------------------------------
    if (e.target.classList.contains("verwijder-soort")) {

        const naam = e.target.dataset.naam;
        if (!naam) return;

        if (!confirm(`Soort "${naam}" verwijderen?`)) return;

        postSoort("/Home/VerwijderSoort", naam);
        return;
    }

    // -------------------------------
    // ➕ SOORT TOEVOEGEN
    // -------------------------------
    if (e.target.classList.contains("voeg-soort-toe")) {

        const naam = prompt("Nieuwe soort naam:");
        if (!naam) return;

        postSoort("/Home/VoegSoortToe", naam);
        return;
    }

    // -------------------------------
    // KLIK BUITEN → ALLE DROPDOWNS DICHT
    // -------------------------------
    document.querySelectorAll(".soortDropdown")
        .forEach(d => d.classList.add("d-none"));
});


// =====================================================
// HULPFUNCTIE POST
// =====================================================
function postSoort(url, naam) {
    const form = document.createElement("form");
    form.method = "post";
    form.action = url;

    const input = document.createElement("input");
    input.type = "hidden";
    input.name = "naam";
    input.value = naam;

    form.appendChild(input);
    document.body.appendChild(form);
    form.submit();
}