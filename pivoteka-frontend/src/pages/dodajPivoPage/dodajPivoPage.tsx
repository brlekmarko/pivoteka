import { InputText } from "primereact/inputtext";
import { useEffect, useState } from "react";
import { Pivo } from "../../models/Pivo";
import { dodajNovoPivo, dohvatiSveDobavljace } from "../../apiCalls/apiCalls";
import { InputNumber } from "primereact/inputnumber";
import { Button } from "primereact/button";
import { Dropdown } from "primereact/dropdown";
import './dodajPivoPage.css';

export default function DodajPivoPage() {
    const [ime, setIme] = useState('');
    const [cijena, setCijena] = useState(0);
    const [kolicina, setKolicina] = useState(0);
    const [opis, setOpis] = useState('');
    const [zemljaPodrijetla, setZemljaPodrijetla] = useState('');
    const [netoVolumen, setNetoVolumen] = useState(0);
    const [imeDobavljaca, setImeDobavljaca] = useState('');
    const [vrsta, setVrsta] = useState();

    const [error, setError] = useState('');

    const [dobavljaci, setDobavljaci] = useState<any[]>([]);


    const dodajPivo = async () => {
        if (ime === '' || cijena === 0 || kolicina === 0 || opis === '' || zemljaPodrijetla === '' || netoVolumen === 0 || imeDobavljaca === '' || !vrsta) {
            setError('Molimo popunite sva polja');
            return;
        }

        setError('');

        try {
            const pivo : Pivo = {
                ime,
                cijena,
                kolicina,
                opis,
                zemlja_podrijetla: zemljaPodrijetla,
                neto_volumen: netoVolumen,
                ime_dobavljaca: imeDobavljaca,
                vrsta
            };
            const res = await dodajNovoPivo(pivo);
            window.location.href = '/piva';
        } catch (error) {
            setError('Greška prilikom dodavanja piva');
        }
    }

    async function dohvatiDobavljace() {
        let sviDobavljaci = await dohvatiSveDobavljace();
        let sviDobavljaciDropdown = sviDobavljaci.map((dobavljac) => {
            return {label: dobavljac, value: dobavljac};
        });
        setDobavljaci(sviDobavljaciDropdown);
    }

    useEffect(() => {
        dohvatiDobavljace();
    }, []);

    return (
        <div className="pivo-forma">
            <h1>Dodaj pivo</h1>
            <div>
                <label>Ime</label>
                <InputText value={ime} onChange={(e) => setIme(e.target.value)} />
            </div>
            <div>
                <label>Cijena (€)</label>
                <InputNumber value={cijena} onChange={(e) => setCijena(e.value ? e.value : -1)} />
            </div>
            <div>
                <label>Količina</label>
                <InputNumber value={kolicina} onChange={(e) => setKolicina(e.value ? e.value : -1)} />
            </div>
            <div>
                <label>Opis</label>
                <InputText value={opis} onChange={(e) => setOpis(e.target.value)} />
            </div>
            <div>
                <label>Zemlja podrijetla</label>
                <InputText value={zemljaPodrijetla} onChange={(e) => setZemljaPodrijetla(e.target.value)} />
            </div>
            <div>
                <label>Neto volumen (ml)</label>
                <InputNumber value={netoVolumen} onChange={(e) => setNetoVolumen(e.value ? e.value : -1)} />
            </div>
            <div>
                <label>Ime dobavljača</label>
                <Dropdown value={imeDobavljaca} showClear options={dobavljaci} onChange={(e) => setImeDobavljaca(e.value)} placeholder="Dobavljac" />
            </div>
            <div>
                <label>Vrsta</label>
                <Dropdown value={vrsta} showClear options={[{label: 'Lager', value: 'Lager'}, {label: 'Ale', value: 'Ale'}, {label: 'Radler', value: 'Radler'}, {label: 'Tamno', value: 'Tamno'}]} onChange={(e) => setVrsta(e.value)} placeholder="Vrsta" />
            </div>
            <Button onClick={dodajPivo} >Dodaj pivo</Button>
            <p>{error}</p>
        </div>
    );
}