import React from 'react';
import './List.css';
import { Disk, DISK_TYPE_DISPLAY, DISK_FORM_DISPLAY } from './types/Disk';

interface ListState {
    disks: Disk[];
}

class List extends React.Component<{ }, ListState> {
    public async componentDidMount(): Promise<void> {
        const response = await fetch('https://localhost:5001/Disk/IndexJson');
        const disks = await response.json() as Disk[];
        this.setState({ disks });
    }

    public render(): React.ReactNode {
        return (
            <div className="disk-table">
                <table className="table">
                    <thead>
                        <tr>
                            <th>Название</th>
                            <th>Размер</th>
                            <th>Скорость записи</th>
                            <th>Скорость чтения</th>
                            <th>Цена</th>
                            <th>Тип</th>
                            <th>Формат</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {this.state?.disks.map(disk => (
                            <tr key={disk.id}>
                                <td className="disk-table__cell-name">{disk.name}</td>
                                <td>{disk.sizeGb} ГБ</td>
                                <td>{disk.writeSpeedMbps} МБ/с</td>
                                <td>{disk.readSpeedMbps} МБ/с</td>
                                <td>{disk.price} ₽</td>
                                <td>{DISK_TYPE_DISPLAY[disk.type]}</td>
                                <td>{DISK_FORM_DISPLAY[disk.form]}</td>
                                <td>
                                    <a
                                        className="disk-table__to-cart"
                                        onClick={() => this.handleCartClick(disk.id)}
                                    >В корзину</a>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }

    private handleCartClick(diskId: number): void {
        fetch('https://localhost:5001/Disk/AddToCart', {
            method: 'POST',
            headers: { ['Content-Type']: 'application/json' },
            body: JSON.stringify(diskId),
            credentials: 'include',
        });
    }
}

export default List;
