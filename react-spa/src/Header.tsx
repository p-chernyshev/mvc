import React from 'react';
import { Link } from 'react-router-dom';
import './Header.css';

interface HeaderProps {
    inCart?: number;
}

class Header extends React.Component<HeaderProps> {
    public render(): React.ReactNode {
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
                                    <Link to="/list" className="header__navbar-link nav-link">Товары</Link>
                                </li>
                                <li className="nav-item mvc-badge__anchor">
                                    <Link to="/cart" className="header__navbar-link nav-link">Корзина</Link>
                                    <div className={`mvc-badge ${this.props.inCart ? 'mvc-badge_visible' : 'mvc-badge_hidden'}`}>
                                        <div><span className="mvc-badge__text">{this.props.inCart}</span></div>
                                    </div>
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
