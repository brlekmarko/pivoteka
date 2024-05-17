import { useEffect, useState } from "react";
import { Narudzba } from "../../models/Narudzba";
import { dohvatiNarudzbu, obrisiNarudzbu } from "../../apiCalls/apiCalls";
import { Button } from "primereact/button";
import './narudzbaPage.css';

export default function NarudzbaPage() {

    const [narudzba, setNarudzba] = useState<Narudzba>();

    async function fetchNarudzba() {
        let id = window.location.pathname.split('/')[2];
        const res = await dohvatiNarudzbu(parseInt(id));
        setNarudzba(res);
    }

    async function izbrisiNarudzbu() {
        let id = window.location.pathname.split('/')[2];
        await obrisiNarudzbu(parseInt(id));
        window.location.href = '/narudzbe';
    }

    useEffect(() => {
        fetchNarudzba();
    }, []);

    return (
        <div>
            <div className="buttons">
                <Button label="Obriši" onClick={izbrisiNarudzbu} />
            </div>
            <div className="narudzba">
                <div className="narudzbaInfo">
                    <h1>Narudžba {narudzba?.id}</h1>
                    <h2>Datum: {narudzba?.datum.split('T')[0]}</h2>
                    <h2>Ukupna cijena (€): {narudzba?.ukupna_cijena}</h2>
                    <h2>Korisničko ime: {narudzba?.korisnicko_ime}</h2>
                </div>
                <div className="narudzbaStavke">
                    <h2>Stavke:</h2>
                    <ul>
                        {narudzba?.stavke.map((stavka, index) => {
                            return (
                                <li key={index}>
                                    <h3>Ime: {stavka.ime_piva}</h3>
                                    <h3>Količina: {stavka.kolicina}</h3>
                                    <h3>Cijena(€): {stavka.cijena_piva}</h3>
                                </li>
                            )
                        })}
                    </ul>
                </div>
            </div>
        </div>
    );
}