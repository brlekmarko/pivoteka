import axios from 'axios';
import { Pivo } from '../models/Pivo';
import { Narudzba } from '../models/Narudzba';

const endpoint = 'https://localhost:7069/api';

export const dohvatiSvaPiva = async () : Promise<Pivo[]> => {
    const response = await axios.get(`${endpoint}/Pivo`);
    return response.data as Pivo[];
    // return [
    //     {
    //         ime: 'Karlovacko',
    //         cijena: 10,
    //         kolicina: 100,
    //         opis: 'Pivo iz Karlovca',
    //         zemlja_podrijetla: 'Hrvatska',
    //         neto_volumen: 500,
    //         ime_dobavljaca: 'Karlovačka pivovara',
    //         vrsta: 'Lager'
    //     },
    //     {
    //         ime: 'Ozujsko',
    //         cijena: 12,
    //         kolicina: 50,
    //         opis: 'Pivo iz Zagreba',
    //         zemlja_podrijetla: 'Hrvatska',
    //         neto_volumen: 500,
    //         ime_dobavljaca: 'Zagrebačka pivovara',
    //         vrsta: 'Lager'
    //     }
    // ]
}

export const dohvatiPivo = async (ime: string) : Promise<Pivo> => {
    const response = await axios.get(`${endpoint}/Pivo/${ime}`);
    return response.data as Pivo;
    // if (ime === 'Karlovacko') {
    //     return {
    //         ime: 'Karlovacko',
    //         cijena: 10,
    //         kolicina: 100,
    //         opis: 'Pivo iz Karlovca',
    //         zemlja_podrijetla: 'Hrvatska',
    //         neto_volumen: 500,
    //         ime_dobavljaca: 'Karlovačka pivovara',
    //         vrsta: 'Lager'
    //     }
    // } else {
    //     return {
    //         ime: 'Ozujsko',
    //         cijena: 12,
    //         kolicina: 50,
    //         opis: 'Pivo iz Zagreba',
    //         zemlja_podrijetla: 'Hrvatska',
    //         neto_volumen: 500,
    //         ime_dobavljaca: 'Zagrebačka pivovara',
    //         vrsta: 'Lager'
    //     }
    // }
}

export const dodajNovoPivo = async (pivo: Pivo) => {
    await axios.post(`${endpoint}/Pivo`, pivo);
}

export const azurirajPivo = async (pivo: Pivo) => {
    await axios.put(`${endpoint}/Pivo/${pivo.ime}`, pivo);
}

export const obrisiPivo = async (ime: string) => {
    await axios.delete(`${endpoint}/Pivo/${ime}`);
}

export const dohvatiSveDobavljace = async () : Promise<any[]> => {
    const response = await axios.get(`${endpoint}/Dobavljac`);
    return response.data as any[];
    //return ['Karlovačka pivovara', 'Zagrebačka pivovara'];
}




export const dohvatiSveNarudzbe = async () : Promise<Narudzba[]> => {
    const response = await axios.get(`${endpoint}/Narudzba/Aggregates`);
    return response.data as Narudzba[];
    // return [
    //     {
    //         id: 1,
    //         datum: new Date().toISOString(),
    //         ukupna_cijena: 100,
    //         korisnicko_ime: 'testuser',
    //         stavke: [
    //             {
    //                 id_narudzbe: 1,
    //                 ime_piva: 'Karlovacko',
    //                 cijena_piva: 10,
    //                 kolicina: 10
    //             },
    //             {
    //                 id_narudzbe: 1,
    //                 ime_piva: 'Ozujsko',
    //                 cijena_piva: 12,
    //                 kolicina: 5
    //             }
    //         ]
    //     }
    // ]
}


export const dohvatiNarudzbu = async (id: number) : Promise<Narudzba> => {
    const response = await axios.get(`${endpoint}/Narudzba/Aggregate/${id}`);
    return response.data as Narudzba;
    // return {
    //     id: 1,
    //     datum: new Date().toISOString(),
    //     ukupna_cijena: 100,
    //     korisnicko_ime: 'testuser',
    //     stavke: [
    //         {
    //             id_narudzbe: 1,
    //             ime_piva: 'Karlovacko',
    //             cijena_piva: 10,
    //             kolicina: 10
    //         },
    //         {
    //             id_narudzbe: 1,
    //             ime_piva: 'Ozujsko',
    //             cijena_piva: 12,
    //             kolicina: 5
    //         }
    //     ]
    // }
}

export const dodajNovuNarudzbu = async (narudzba: Narudzba) => {
    await axios.post(`${endpoint}/Narudzba/Aggregate`, narudzba);
}

export const obrisiNarudzbu = async (id: number) => {
    await axios.delete(`${endpoint}/Narudzba/${id}`);
}


export const dohvatiSveKorisnike = async () : Promise<any[]> => {
    const response = await axios.get(`${endpoint}/Korisnik`);
    return response.data as any[];
    // return ['testuser', 'testuser2'];
}