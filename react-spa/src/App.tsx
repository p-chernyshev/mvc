import React from 'react';
import './App.css';
import { Route, Redirect, RouteComponentProps, withRouter } from 'react-router-dom';
import Cart from './Cart';
import Header from './Header';
import List from './List';
import { CartActionResponse } from './types/Cart';

interface AppState {
    inCart?: number;
}

class App extends React.Component<RouteComponentProps, AppState> {
    public async componentDidMount(): Promise<void> {
        const response = await fetch('https://localhost:5001/Disk/CartLengthJson', { credentials: 'include' });
        this.setState({ inCart: await response.json() });
    }

    public render(): React.ReactNode {
        return (
            <>
                <Header inCart={this.state?.inCart}/>
                <div className="container">
                    <main role="main" className="pb-3">
                        <Route exact path=""> <Redirect to="/list"/> </Route>
                        <Route path="/list">
                            <List onDickClickToCart={diskId => this.handleDiskClickToCart(diskId)}/>
                        </Route>
                        <Route path="/cart">
                            <Cart onCheckoutSuccessful={() => this.handleCheckoutSuccessful()}/>
                        </Route>
                    </main>
                </div>
            </>
        );
    }

    private async handleDiskClickToCart(diskId: number): Promise<void> {
        const response = await fetch('https://localhost:5001/Disk/AddToCart', {
            method: 'POST',
            headers: { ['Content-Type']: 'application/json' },
            body: JSON.stringify(diskId),
            credentials: 'include',
        });
        const cartActionResponse: CartActionResponse = await response.json();
        this.setState({ inCart: cartActionResponse.length });
    }

    private handleCheckoutSuccessful(): void {
        this.props.history.push('/list');
        this.setState({ inCart: undefined });
    }
}

export default withRouter(App);
