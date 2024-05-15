import './App.css';
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/homePage/homePage';
import SvaPivaPage from './pages/svaPivaPage/svaPivaPage';
import SveNarudzbePage from './pages/sveNarudzbePage/sveNarudzbePage';
import PivoPage from './pages/pivoPage/pivoPage';
import NarudzbaPage from './pages/narudzbaPage/narudzbaPage';
import { Dialog } from 'primereact/dialog';
import { useCallback, useState } from 'react';
import { BsCart4 } from "react-icons/bs";
import { Button } from 'primereact/button';
import { dodajNovuNarudzbu } from './apiCalls/apiCalls';
import DodajPivoPage from './pages/dodajPivoPage/dodajPivoPage';
import UrediPivoPage from './pages/urediPivoPage/urediPivoPage';

function App() {
  

  const [kosarica, setKosarica] = useState<boolean>(false);
  const [refresh, setRefresh] = useState<boolean>(false);

  const generateCart = useCallback(() => {
    let kosarica = localStorage.getItem('kosarica');
    let ukupna_cijena = 0;

    if(kosarica === null || kosarica === '[]') {
      return <p>Košarica je prazna</p>
    } else {
      let stavke = JSON.parse(kosarica);
      return (
        <div>
          {stavke.map((stavka: any, index: number) => {
            ukupna_cijena += stavka.kolicina * stavka.cijena_piva;
            return (
              <div className="cart-item" key={index}>
                <h1>{stavka.ime_piva}</h1>
                <h1>{stavka.kolicina}</h1>
                <Button label="X" onClick={() => {
                    let kosarica = JSON.parse(localStorage.getItem('kosarica')!);
                    kosarica = kosarica.filter((stavka2: any) => stavka2.ime_piva !== stavka.ime_piva);
                    localStorage.setItem('kosarica', JSON.stringify(kosarica));
                    setRefresh(!refresh);
                  }
                } />
              </div>
            )
          })}
          <h1>Ukupna cijena: {ukupna_cijena}</h1>
          <Button label="Naruči" onClick={() => naruci()} />
        </div>
      )
    }
  }
  , [refresh]);

  function naruci(){
    dodajNovuNarudzbu({
      id: -1,
      datum: new Date().toISOString(),
      ukupna_cijena: -1,
      korisnicko_ime: 'testuser',
      stavke: JSON.parse(localStorage.getItem('kosarica')!),
    });
    localStorage.removeItem('kosarica');
    setKosarica(false);
  }

  return (
    <div className="App">
      <header className="App-header">
        <a href={"/"}>Home</a>
        <a href={"/piva"}>Piva</a>
        <a href={"/narudzbe"}>Narudžbe</a>
        <BsCart4 className="cartIcon" onClick={() => setKosarica(true)}></BsCart4>
        <Dialog header="Košarica" visible={kosarica} onHide={() => {setKosarica(false)}}>
          {generateCart()}
        </Dialog>
      </header>

      <Routes>
        <Route
          path={"/"}
          element={<HomePage/>}
        />
        <Route
            path={"/piva"}
            element={<SvaPivaPage/>}
        />
        <Route
            path={"/narudzbe"}
            element={<SveNarudzbePage/>}
        />
        <Route
            path={"/piva/:ime"}
            element={<PivoPage />}
        />
        <Route
            path={"/narudzbe/:id"}
            element={<NarudzbaPage />}
        />
        <Route
            path={"/dodajPivo"}
            element={<DodajPivoPage />}
        />
      <Route
            path={"/urediPivo/:ime"}
            element={<UrediPivoPage />}
        />
    </Routes>
    </div>
  );
}

export default App;
