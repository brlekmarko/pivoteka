import { render, screen } from "@testing-library/react";
import PivoPage from "./pivoPage";

test('check if cijena renders', async () => {
    render(<PivoPage />);

    //check if cijena is in the document
    const element = screen.getByText(/Cijena/i);

    expect(element).toBeInTheDocument();
});


test('check if Volumen renders', async () => {
    render(<PivoPage />);

    //check if volumen is in the document
    const element = screen.getByText(/Volumen/i);

    expect(element).toBeInTheDocument();
});