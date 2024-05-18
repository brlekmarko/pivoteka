import { render, screen } from "@testing-library/react";
import UrediPivoPage from "./urediPivoPage";

test('check if Uredi pivo text renders', async () => {
    render(<UrediPivoPage />);

    const element = screen.getByText(/Uredi pivo/i);

    expect(element).toBeInTheDocument();
});