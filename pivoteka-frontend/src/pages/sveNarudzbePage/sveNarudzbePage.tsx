import { useCallback, useEffect, useState } from "react";
import { Narudzba } from "../../models/Narudzba";
import { dohvatiSveKorisnike, dohvatiSveNarudzbe } from "../../apiCalls/apiCalls";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";

export default function SveNarudzbePage() {

    const [narudzbe, setNarudzbe] = useState<Narudzba[]>([]);
    const [selectedKorisnik, setSelectedKorisnik] = useState<string>();
    const [sviKorisnici, setSviKorisnici] = useState<any[]>([]);

    async function fetchKorisnici() {
        const response = await dohvatiSveKorisnike();
        setSviKorisnici(response.map((korisnik: any) => {
            return {label: korisnik.korisnicko_ime, value: korisnik.korisnicko_ime}
        }));
    }

    async function fetchNarudzbe() {
        const res = await dohvatiSveNarudzbe();
        setNarudzbe(res);
    }

    const filtriraneNarudzbe = useCallback((narudzbe: Narudzba[]) => {
        let currentNarudzbe = narudzbe;
        if(selectedKorisnik) {
            currentNarudzbe = currentNarudzbe.filter((narudzba) => narudzba.korisnicko_ime === selectedKorisnik);
        }
        return currentNarudzbe;
    }
    , [selectedKorisnik, narudzbe]);

    useEffect(() => {
        fetchNarudzbe();
        fetchKorisnici();
    }, []);

    return (
        <div>
            <h1>Popis svih narudžbi</h1>

            <div className="searchbar">
                <Dropdown value={selectedKorisnik} showClear options={sviKorisnici} onChange={(e) => setSelectedKorisnik(e.value)} placeholder="Korisnik" />
            </div>


            <DataTable value={filtriraneNarudzbe(narudzbe)} selectionMode="single" onRowClick={(e) => window.location.href=`/narudzbe/${e.data.id}`}>
                <Column field="id" header="ID" sortable></Column>
                <Column field="datum" header="Datum" sortable body={(rowData: any) => rowData.datum.split('T')[0]}></Column>
                <Column field="ukupna_cijena" header="Ukupna cijena (€)" sortable></Column>
                <Column field="korisnicko_ime" header="Korisnicko ime"></Column>
            </DataTable>
        </div>
    );
}