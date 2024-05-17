import { useCallback, useEffect, useState } from "react";
import { Pivo } from "../../models/Pivo";
import { dohvatiSvaPiva } from "../../apiCalls/apiCalls";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { InputText } from "primereact/inputtext";
import { Dropdown } from "primereact/dropdown";
import { StavkaNarudzbe } from "../../models/StavkaNarudzbe";
import { Button } from "primereact/button";
import './svaPivaPage.css';
import { Store } from 'react-notifications-component';

export default function SvaPivaPage() {

    const [piva, setPiva] = useState<Pivo[]>([]);
    const [search, setSearch] = useState<string>('');
    const [selectedVrsta, setSelectedVrsta] = useState<string>();

    async function fetchPiva() {
        const res = await dohvatiSvaPiva();
        setPiva(res);
    }

    const filterPiva = useCallback((piva : Pivo[]) => {
        let currentPiva = piva;
        if (search !== '') {
            currentPiva = currentPiva.filter((pivo) => pivo.ime.toLowerCase().includes(search.toLowerCase()));
        } 
        if (selectedVrsta) {
            currentPiva = currentPiva.filter((pivo) => pivo.vrsta === selectedVrsta);
        }
        return currentPiva;
    }, [search, selectedVrsta]);

    function showNotification() {
        Store.addNotification({
            title: "Narudžba uspješno dodana",
            message: "Narudžba je uspješno dodana u košaricu",
            type: "success",
            insert: "top",
            container: "top-right",
            dismiss: {
                duration: 2000,
                onScreen: true
            }
        });
    }

    function dodajUKosaricu(pivo: Pivo) {
        let stavkaNarudzbe : StavkaNarudzbe = {
            id_narudzbe: 0,
            ime_piva: pivo.ime,
            cijena_piva: pivo.cijena,
            kolicina: 1
        }
        if(localStorage.getItem('kosarica') === null) {
            localStorage.setItem('kosarica', JSON.stringify([stavkaNarudzbe]));
        } else {
            const kosarica = JSON.parse(localStorage.getItem('kosarica')!);
            if(kosarica.find((stavka: StavkaNarudzbe) => stavka.ime_piva === stavkaNarudzbe.ime_piva)) {
                kosarica.forEach((stavka: StavkaNarudzbe) => {
                    if(stavka.ime_piva === stavkaNarudzbe.ime_piva) {
                        stavka.kolicina += 1;
                    }
                });
            } else{
                kosarica.push(stavkaNarudzbe);
            }
            localStorage.setItem('kosarica', JSON.stringify(kosarica));
        }
        showNotification();
    }

    useEffect(() => {
        fetchPiva();
    }, []);

    return (
        <div>
            <div className="searchbar">
                <Button label="Dodaj novo pivo" onClick={() => window.location.href = '/dodajPivo'} />
                <InputText placeholder="Pretraži piva" value={search} onChange={(e) => setSearch(e.target.value)} />
                <Dropdown value={selectedVrsta} showClear options={[{label: 'Lager', value: 'Lager'}, {label: 'Ale', value: 'Ale'}, {label: 'Radler', value: 'Radler'}, {label: 'Tamno', value: 'Tamno'}]} onChange={(e) => setSelectedVrsta(e.value)} placeholder="Vrsta" />
            </div>

            <DataTable value={filterPiva(piva)}>
                <Column header="Ime" body={(rowData: Pivo) => <a href={`/piva/${rowData.ime}`} target="_blank" rel="noreferrer">{rowData.ime}</a>}></Column>
                <Column field="neto_volumen" header="Volumen (ml)"></Column>
                <Column field="cijena" header="Cijena (€)"></Column>
                <Column field="vrsta" header="Vrsta"></Column>
                <Column header="Naruči" body={(rowData: Pivo) => <Button onClick={() => dodajUKosaricu(rowData)}>Naruči</Button>}></Column>
            </DataTable>
        </div>
    );
}