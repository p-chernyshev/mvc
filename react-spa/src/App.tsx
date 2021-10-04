import React from 'react';
import './App.css';
import { Route, Redirect } from 'react-router-dom';
import Header from './Header';
import List from './List';

class App extends React.Component {
    public render(): React.ReactNode {
        return (
            <>
                <Header />
                <div className="container">
                    <main role="main" className="pb-3">
                        <Route exact path=""> <Redirect to="/list"/> </Route>
                        <Route path="/list">
                            <List />
                        </Route>
                        <Route path="/cart">
                        </Route>
                    </main>
                </div>
            </>
        );
    }
}

export default App;
