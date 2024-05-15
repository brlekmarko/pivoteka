import './App.css';
import { Routes, Route } from 'react-router-dom';

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
