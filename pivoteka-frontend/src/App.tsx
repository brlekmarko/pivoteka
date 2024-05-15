import './App.css';
import { Routes, Route } from 'react-router-dom';
import HomePage from './pages/homePage/homePage';
import SvaPivaPage from './pages/svaPivaPage/svaPivaPage';
import SveNarudzbePage from './pages/sveNarudzbePage/sveNarudzbePage';
import PivoPage from './pages/pivoPage/pivoPage';
import NarudzbaPage from './pages/narudzbaPage/narudzbaPage';

function App() {
  return (
    <div className="App">
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
    </Routes>
    </div>
  );
}

export default App;
