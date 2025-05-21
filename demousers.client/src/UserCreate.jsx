import { useState } from 'react';

function UserCreate({ onUserCreated, onCancel }) {
    const [name, setName] = useState('');
    const [email, setEmail] = useState('');
    const [image, setImageUrl] = useState('');
    const [error, setError] = useState(null);
    const [loading, setLoading] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();
        setError(null);
        setLoading(true);
        const res = await fetch('users', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ name, email, image })
        });

        setLoading(false);
        if (!res.ok) {
            setError('Failed to create user.');
            return;
        }

        onUserCreated();
    }

    return (
        <div style={{
            background: 'none',
            minHeight: '100vh',
            padding: 32,
            fontFamily: 'Segoe UI, sans-serif'
        }}>
            <button onClick={onCancel} style={{
                background: 'none',
                color: '#b9bbbe',
                border: 'none',
                fontSize: 16,
                cursor: 'pointer',
                marginBottom: 16
            }}>&larr; Back to list</button>
            <div style={{
                background: '#18191c',
                borderRadius: 16,
                maxWidth: 420,
                margin: '0 auto',
                boxShadow: '0 2px 10px #00000040',
                padding: 0
            }}>
                <div style={{
                    padding: '32px 32px 16px 32px',
                    borderTopLeftRadius: 16,
                    borderTopRightRadius: 16,
                    background: '#232428'
                }}>
                    <div style={{
                        color: '#fff',
                        fontWeight: 600,
                        fontSize: 22,
                        lineHeight: 1.2
                    }}>
                        Create New User
                    </div>
                </div>
                <form onSubmit={handleSubmit} style={{ padding: 32 }}>
                    <div style={{ marginBottom: 20 }}>
                        <div style={{ color: '#fff', fontWeight: 600, fontSize: 15 }}>Display Name</div>
                        <input
                            type="text"
                            value={name}
                            onChange={e => setName(e.target.value)}
                            required
                            style={{
                                width: '50%',
                                padding: 8,
                                borderRadius: 6,
                                border: '1px solid #36393f',
                                background: '#232428',
                                color: '#fff',
                                fontSize: 15,
                                marginTop: 4
                            }}
                        />
                    </div>
                    <div style={{ marginBottom: 20 }}>
                        <div style={{ color: '#fff', fontWeight: 600, fontSize: 15 }}>E-Mail Address</div>
                        <input
                            type="email"
                            value={email}
                            onChange={e => setEmail(e.target.value)}
                            required
                            style={{
                                width: '50%',
                                padding: '4px 8px',
                                borderRadius: 0,
                                border: '1px solid #36393f',
                                background: '#232428',
                                color: '#fff',
                                fontSize: 15,
                                marginTop: 4
                            }}
                        />
                    </div>
                    <div style={{ marginBottom: 20 }}>
                        <div style={{ color: '#fff', fontWeight: 600, fontSize: 15 }}>Image URL</div>
                        <input
                            type="url"
                            value={image}
                            onChange={e => setImageUrl(e.target.value)}
                            placeholder="https://example.com/image.jpg"
                            style={{
                                width: '50%',
                                padding: 8,
                                borderRadius: 6,
                                border: '1px solid #36393f',
                                background: '#232428',
                                color: '#fff',
                                fontSize: 15,
                                marginTop: 4
                            }}
                        />
                    </div>
                    {error && (
                        <div style={{ color: '#f04747', marginBottom: 12 }}>
                            {error}
                        </div>
                    )}
                    <div style={{ display: 'flex', gap: 8 }}>
                        <button
                            className="btn"
                            type="submit"
                            disabled={loading}
                            style={{
                                background: '#43b581',
                                color: '#fff',
                                border: 'none',
                                borderRadius: 6,
                                padding: '8px 20px',
                                fontWeight: 600,
                                cursor: loading ? 'not-allowed' : 'pointer'
                            }}
                        >Create</button>
                        <button
                            className="btn"
                            type="button"
                            onClick={onCancel}
                            disabled={loading}
                            style={{
                                background: '#f04747',
                                color: '#fff',
                                border: 'none',
                                borderRadius: 6,
                                padding: '8px 20px',
                                fontWeight: 600,
                                cursor: loading ? 'not-allowed' : 'pointer'
                            }}
                        >Cancel</button>
                    </div>
                </form>
            </div>
        </div>
    );
}

export default UserCreate;