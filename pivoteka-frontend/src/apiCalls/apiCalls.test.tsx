import { dohvatiSveKorisnike } from "./apiCalls";

test('checks result of fetching all users', async () => {
    const res = await dohvatiSveKorisnike();
    //check if it contains testkupac1 as username
    expect(res.filter((korisnik: any) => korisnik.username === 'testkupac1')).toHaveLength(1);
});


