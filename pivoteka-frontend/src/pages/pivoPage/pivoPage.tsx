import { useEffect, useState } from "react";
import { Pivo } from "../../models/Pivo";
import { dohvatiPivo, obrisiPivo } from "../../apiCalls/apiCalls";
import { Button } from "primereact/button";
import './pivoPage.css';

export default function PivoPage() {

    const [pivo, setPivo] = useState<Pivo>();

    async function dobaviPivo() {
        let ime = window.location.pathname.split('/')[2];
        let pivo = await dohvatiPivo(ime);
        setPivo(pivo);
    }

    async function urediPivo() {
        let ime = window.location.pathname.split('/')[2];
        window.location.href = `/urediPivo/${ime}`;
    }

    async function izbrisiPivo() {
        let ime = window.location.pathname.split('/')[2];
        const res = await obrisiPivo(ime);
        window.location.href = '/piva';
    }

    useEffect(() => {
        dobaviPivo();
    }, []);

    return (
        <div>
            <div className="pivo-buttons">
                <Button label="Uredi" onClick={urediPivo} />
                <Button label="Obriši" onClick={izbrisiPivo} />
            </div>

            <div className="pivo-info">
                <h1>Ime: {pivo?.ime}</h1>
                <h1>Cijena (€): {pivo?.cijena}</h1>
                <h1>Količina: {pivo?.kolicina}</h1>
                <h1>Opis: {pivo?.opis}</h1>
                <h1>Zemlja podrijetla: {pivo?.zemlja_podrijetla}</h1>
                <h1>Volumen (ml): {pivo?.neto_volumen}</h1>
                <h1>Dobavljač: {pivo?.ime_dobavljaca}</h1>
                <h1>Vrsta: {pivo?.vrsta}</h1>
            </div>
        </div>
    );
}