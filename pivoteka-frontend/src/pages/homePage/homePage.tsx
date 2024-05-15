import { Button } from 'primereact/button';

export default function HomePage() {
    return (
        <div>
            <div>
                <span>
                    <h1>Naručite jedno od svjetski poznatih piva</h1>
                    <Button label="Naruči" onClick={() => window.location.href = '/piva'} />
                </span>

                <span>
                    <h1>Pregledajte sve narudžbe</h1>
                    <Button label="Pregledaj" onClick={() => window.location.href = '/narudzbe'} />
                </span>
            </div>
            <img src={process.env.PUBLIC_URL + '/pivoteka.png'} alt='logo-pivoteke' />
        </div>
    );
}