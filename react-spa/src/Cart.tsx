import React from 'react';
import './Cart.css';
import { CartSession } from './types/Cart';

interface CartState {
    cart: CartSession
}

export default class Cart extends React.Component<{ }, CartState> {
    public async componentDidMount(): Promise<void> {
        const response = await fetch('https://localhost:5001/Disk/CartJson', { credentials: 'include' });
        this.setState({ cart: await response.json() });
    }

    public render(): React.ReactNode {
        return (
            <div className="cart">
                <table className="table">
                    <thead>
                        <tr>
                            <th>Название</th>
                            <th>Цена</th>
                            <th>Количество</th>
                            <th>Всего</th>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state?.cart?.entries.map(diskEntry => (
                            <tr key={diskEntry.disk.id}>
                                <td className="cart__cell-name">
                                    {diskEntry.disk.name}
                                </td>
                                <td>
                                    {diskEntry.disk.price} ₽
                                </td>
                                <td>
                                    {diskEntry.count}
                                </td>
                                <td>
                                    {diskEntry.total}
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                <div className="cart__result">
                    <h4 className="cart__total">
                        Сумма:
                        <span className="cart__total-sum">
                            {this.state?.cart?.total}
                        </span>
                        ₽
                    </h4>

                    <input
                        className="mvc-button cart__checkout"
                        type="submit"
                        value="Оформить покупку"
                    />
                </div>
            </div>
        );
    }
}
