import axios from 'axios';
import { Pivo } from '../models/Pivo';
import { Narudzba } from '../models/Narudzba';

const endpoint = 'http://localhost:4000';

export const dohvatiSvaPiva = async () : Promise<Pivo[]> => {
    //const response = await axios.get(`${endpoint}/piva`);
    //return response.data as Pivo[];
    return [
        {
            ime: 'Karlovacko',
            cijena: 10,
            kolicina: 100,
            opis: 'Pivo iz Karlovca',
            zemlja_podrijetla: 'Hrvatska',
            neto_volumen: 500,
            ime_dobavljaca: 'Karlovačka pivovara',
            vrsta: 'Lager'
        },
        {
            ime: 'Ozujsko',
            cijena: 12,
            kolicina: 50,
            opis: 'Pivo iz Zagreba',
            zemlja_podrijetla: 'Hrvatska',
            neto_volumen: 500,
            ime_dobavljaca: 'Zagrebačka pivovara',
            vrsta: 'Lager'
        }
    ]
}

export const dohvatiPivo = async (ime: string) : Promise<Pivo> => {
    //const response = await axios.get(`${endpoint}/piva/${ime}`);
    //return response.data as Pivo;
    if (ime === 'Karlovacko') {
        return {
            ime: 'Karlovacko',
            cijena: 10,
            kolicina: 100,
            opis: 'Pivo iz Karlovca',
            zemlja_podrijetla: 'Hrvatska',
            neto_volumen: 500,
            ime_dobavljaca: 'Karlovačka pivovara',
            vrsta: 'Lager'
        }
    } else {
        return {
            ime: 'Ozujsko',
            cijena: 12,
            kolicina: 50,
            opis: 'Pivo iz Zagreba',
            zemlja_podrijetla: 'Hrvatska',
            neto_volumen: 500,
            ime_dobavljaca: 'Zagrebačka pivovara',
            vrsta: 'Lager'
        }
    }
}

export const dodajNovoPivo = async (pivo: Pivo) => {
    await axios.post(`${endpoint}/piva`, pivo);
}

export const azurirajPivo = async (pivo: Pivo) => {
    await axios.put(`${endpoint}/piva/${pivo.ime}`, pivo);
}

export const obrisiPivo = async (ime: string) => {
    await axios.delete(`${endpoint}/piva/${ime}`);
}

export const dohvatiSveDobavljace = async () : Promise<string[]> => {
    //const response = await axios.get(`${endpoint}/dobavljaci`);
    //return response.data as string[];
    return ['Karlovačka pivovara', 'Zagrebačka pivovara'];
}




export const dohvatiSveNarudzbe = async () : Promise<Narudzba[]> => {
    //const response = await axios.get(`${endpoint}/narudzbe`);
    //return response.data as Narudzba[];
    return [
        {
            id: 1,
            datum: new Date().toISOString(),
            ukupna_cijena: 100,
            korisnicko_ime: 'testuser',
            stavke: [
                {
                    id_narudzbe: 1,
                    ime_piva: 'Karlovacko',
                    cijena_piva: 10,
                    kolicina: 10
                },
                {
                    id_narudzbe: 1,
                    ime_piva: 'Ozujsko',
                    cijena_piva: 12,
                    kolicina: 5
                }
            ]
        }
    ]
}


export const dohvatiNarudzbu = async (id: number) : Promise<Narudzba> => {
    //const response = await axios.get(`${endpoint}/narudzbe/${id}`);
    //return response.data as Narudzba;
    return {
        id: 1,
        datum: new Date().toISOString(),
        ukupna_cijena: 100,
        korisnicko_ime: 'testuser',
        stavke: [
            {
                id_narudzbe: 1,
                ime_piva: 'Karlovacko',
                cijena_piva: 10,
                kolicina: 10
            },
            {
                id_narudzbe: 1,
                ime_piva: 'Ozujsko',
                cijena_piva: 12,
                kolicina: 5
            }
        ]
    }
}

export const dodajNovuNarudzbu = async (narudzba: Narudzba) => {
    await axios.post(`${endpoint}/narudzbe`, narudzba);
}

export const obrisiNarudzbu = async (id: number) => {
    await axios.delete(`${endpoint}/narudzbe/${id}`);
}