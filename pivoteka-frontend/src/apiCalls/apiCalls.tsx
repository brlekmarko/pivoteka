import axios from 'axios';
import { Pivo } from '../models/Pivo';
import { Narudzba } from '../models/Narudzba';

const endpoint = 'http://localhost:4000';

export const dohvatiSvaPiva = async () : Promise<Pivo[]> => {
    const response = await axios.get(`${endpoint}/piva`);
    return response.data as Pivo[];
}

export const dohvatiPivo = async (id: number) : Promise<Pivo> => {
    const response = await axios.get(`${endpoint}/piva/${id}`);
    return response.data as Pivo;
}

export const dodajNovoPivo = async (pivo: Pivo) => {
    await axios.post(`${endpoint}/piva`, pivo);
}

export const azurirajPivo = async (pivo: Pivo) => {
    await axios.put(`${endpoint}/piva/${pivo.ime}`, pivo);
}




export const dohvatiSveNarudzbe = async () : Promise<Narudzba> => {
    const response = await axios.get(`${endpoint}/narudzbe`);
    return response.data as Narudzba;
}

export const dodajNovuNarudzbu = async (narudzba: Narudzba) => {
    await axios.post(`${endpoint}/narudzbe`, narudzba);
}

export const obrisiNarudzbu = async (id: number) => {
    await axios.delete(`${endpoint}/narudzbe/${id}`);
}