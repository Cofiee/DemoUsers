import { useState } from 'react';

function UserCreate({ onUserCreated, onCancel }) {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [error, setError] = useState(null);

    async function handleSubmit(e) {
        e.preventDefault();
        setError(null);
        const res = await fetch('users', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, email })
        });
        if (!res.ok) {
            setError('Failed to create user.');
            return;
        }

        onUserCreated();
    }

    return (
        <form onSubmit={handleSubmit} style={{ marginTop: '2em' }}>
            <h2>Create New User</h2>
            {error && <div style={{ color: 'red' }}>{error}</div>}
            <div className="mb-3">
                <label className="form-label">Name</label>
                <input
                    className="form-control"
                    value={name}
                    onChange={e => setName(e.target.value)}
                    required
                />
            </div>
            <div className="mb-3">
                <label className="form-label">Email</label>
                <input
                    className="form-control"
                    type="email"
                    value={email}
                    onChange={e => setEmail(e.target.value)}
                    required
                />
            </div>
            <button className="btn btn-primary" type="submit">Create</button>
            <button className="btn btn-secondary ms-2" type="button" onClick={onCancel}>Cancel</button>
        </form>
    );
}

export default UserCreate;