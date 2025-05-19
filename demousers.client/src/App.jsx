import { useEffect, useState } from 'react';
import './App.css';

function App() {
    const [users, setUsers] = useState();
    const [selectedUser, setSelectedUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        populateUsers();
    }, []);

    const contents = users === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <div className="container">
            {!selectedUser ? (
                <>
                    <h2>User List</h2>
                        <ul>
                            {users.map(user => (
                                <li key={user.id}>
                                    <button onClick={() => handleUserClick(user.id)} style={{ background: 'none', border: 'none', color: 'blue', textDecoration: 'underline', cursor: 'pointer' }}>
                                        {user.name}
                                    </button>
                                </li>
                            ))}
                        </ul>
                    </>
                ) : (
                    <div>
                        <button onClick={handleBack}>&larr; Back to list</button>
                        <h2>User Detail</h2>
                        <p><b>Name:</b> {selectedUser.name}</p>
                        <p><b>Email:</b> {selectedUser.email}</p>
                        {selectedUser.image && (
                            <p>
                                <b>Image:</b><br />
                                <img src={selectedUser.image} alt="User" style={{ width: 100, height: 100, objectFit: 'cover', borderRadius: '50%' }} />
                            </p>
                        )}
                    </div>
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
            <h1 id="tableLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
        </div>
    );

    async function populateUsers() {
        //setLoading(true);
        const res = await fetch('users');
        if (res.ok) {
            const data = await res.json();
            setUsers(data);
        }
        //setLoading(false);
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
}

export default App;