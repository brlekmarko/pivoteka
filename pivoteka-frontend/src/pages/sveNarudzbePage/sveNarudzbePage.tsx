import { useEffect, useState } from "react";
import { Narudzba } from "../../models/Narudzba";
import { dohvatiSveNarudzbe } from "../../apiCalls/apiCalls";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

export default function SveNarudzbePage() {

    const [narudzbe, setNarudzbe] = useState<Narudzba[]>([]);

    async function fetchNarudzbe() {
        const res = await dohvatiSveNarudzbe();
        setNarudzbe(res);
    }

    useEffect(() => {
        fetchNarudzbe();
    }, []);

    return (
        <div>
            <h1>Popis svih narudžbi</h1>
            <DataTable value={narudzbe} selectionMode="single" onRowClick={(e) => window.location.href=`/narudzbe/${e.data.id}`}>
                <Column field="id" header="ID" sortable></Column>
                <Column field="datum" header="Datum" sortable></Column>
                <Column field="ukupna_cijena" header="Ukupna cijena (€)" sortable></Column>
                <Column field="korisnicko_ime" header="Korisnicko ime"></Column>
            </DataTable>
        </div>
    );
}