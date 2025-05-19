import { useEffect, useState } from 'react';
import './App.css';
import UserList from './UserList';
import UserDetail from './UserDetail';
import UserCreate from './UserCreate';

function App() {
    const [users, setUsers] = useState();
    const [selectedUser, setSelectedUser] = useState(null);
    const [loading, setLoading] = useState(true);
    const [showCreate, setShowCreate] = useState(false);

    useEffect(() => {
        populateUsers();
    }, []);

    const contents = users === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div className="container">
            {showCreate ? (
                <UserCreate
                    onUserCreated={handleUserCreated}
                    onCancel={() => setShowCreate(false)}
                />
            ) : !selectedUser ? (
                <UserList users={users} onUserClick={handleUserClick} />
            ) : (
                <UserDetail user={selectedUser} onBack={handleBack} />
            )}
            <style>{`
                .container {
                    max-width: 600px;
                    margin: 2em auto;
                }
                ul {
                    list-style: none;
                    padding: 0;
                }
                li {
                    margin-bottom: 0.5em;
                }
            `}</style>
        </div>
        ;

    return (
        <div>
            <div className="navbar navbar-expand-lg fixed-top" style={{ backgroundColor: '#6f42c1', color: 'white' }}>
                <div className="container-fluid">
                    <span className="navbar-brand mb-0 h1" style={{ color: 'white' }}>Demo Users</span>
                    <div className="ms-auto">
                        <button
                            className="btn btn-light"
                            style={{ color: '#6f42c1', fontWeight: 'bold' }}
                            onClick={() => setShowCreate(true)}
                        >
                            Add New User
                        </button>
                    </div>
                </div>
            </div>
            <div style={{ paddingTop: '70px' }}>
                {contents}
            </div>
        </div>
    );

    async function populateUsers() {
        setLoading(true);
        const res = await fetch('users');
        if (res.ok) {
            const data = await res.json();
            setUsers(data);
        }
        setLoading(false);
    }

    async function fetchUserDetail(id) {
        setLoading(true);
        const res = await fetch(`users/${id}`);
        if (res.ok) {
            setSelectedUser(await res.json());
        }
        setLoading(false);
    }

    function handleUserClick(id) {
        fetchUserDetail(id);
    }

    function handleBack() {
        setSelectedUser(null);
    }

    function handleUserCreated() {
        setShowCreate(false);
        populateUsers()
    }
}

export default App;