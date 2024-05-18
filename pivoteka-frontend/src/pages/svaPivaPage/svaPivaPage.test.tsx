import { render, screen } from "@testing-library/react";
import SvaPivaPage from "./svaPivaPage";

test('check if button naruči renders', async () => {
    render(<SvaPivaPage />);

    //check if button is in the document
    const element = screen.getByText(/Naruči/i);

    expect(element).toBeInTheDocument();
});