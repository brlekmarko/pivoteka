import { render, screen } from "@testing-library/react";
import SveNarudzbePage from "./sveNarudzbePage";

test('check if price in Euros', async () => {
    render(<SveNarudzbePage />);

    const element = screen.getByText(/Popis svih narudžbi/i);

    expect(element).toBeInTheDocument();
});