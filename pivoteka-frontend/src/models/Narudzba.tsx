import { StavkaNarudzbe } from "./StavkaNarudzbe";

export interface Narudzba {
    id: number;
    datum: string;
    ukupna_cijena: number;
    stavke: StavkaNarudzbe[];
    korisnicko_ime: string;
}