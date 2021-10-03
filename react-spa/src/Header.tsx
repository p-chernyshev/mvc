import React from 'react';
import './Header.css';

class Header extends React.Component {
    public render(): JSX.Element {
        return (
            <header className="header">
                <nav className="header__navbar navbar navbar-expand-sm navbar-toggleable-sm navbar-dark mb-3">
                    <div className="container">
                        <button
                            className="navbar-toggler"
                            type="button"
                            data-toggle="collapse"
                            data-target=".navbar-collapse"
                        >
                            <span className="navbar-toggler-icon"></span>
                        </button>
                        <div className="navbar-collapse collapse d-sm-inline-flex justify-content-between">
                            <ul className="navbar-nav flex-grow-1">
                                <li className="nav-item">
                                    <a className="header__navbar-link nav-link">Товары</a>
                                </li>
                                <li className="nav-item mvc-badge__anchor">
                                    <a className="header__navbar-link nav-link">Корзина</a>
                                </li>
                            </ul>
                        </div>
                    </div>
                </nav>
            </header>
        );
    }
}

export default Header;