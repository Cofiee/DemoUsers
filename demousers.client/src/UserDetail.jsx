import React, { useState } from 'react';

export default function UserDetail({ user, onBack, onUpdate }) {
    const [editMode, setEditMode] = useState(false);
    const [formData, setFormData] = useState({ name: user.name, email: user.email });
    const [loading, setLoading] = useState(false);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleEdit = () => {
        setFormData({ name: user.name, email: user.email });
        setEditMode(true);
    };

    const handleCancel = () => {
        setFormData({ name: user.name, email: user.email });
        setEditMode(false);
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setLoading(true);
        if (onUpdate) {
            await onUpdate(formData);
        }
        setEditMode(false);
        setLoading(false);
    };

    return (
        <div style={{
            background: 'none',
            minHeight: '100vh',
            padding: 32,
            fontFamily: 'Segoe UI, sans-serif'
        }}>
            <button onClick={onBack} style={{
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
                    display: 'flex',
                    alignItems: 'center',
                    padding: '32px 32px 16px 32px',
                    borderTopLeftRadius: 16,
                    borderTopRightRadius: 16,
                    background: '#232428'
                }}>
                    <div style={{ position: 'relative' }}>
                        <img
                            src={user.image}
                            alt="User"
                            style={{
                                width: 80,
                                height: 80,
                                objectFit: 'cover',
                                borderRadius: '50%',
                                border: '4px solid #18191c'
                            }}
                        />
                    </div>
                    <div style={{ marginLeft: 24 }}>
                        <div style={{
                            color: '#fff',
                            fontWeight: 600,
                            fontSize: 22,
                            lineHeight: 1.2
                        }}>
                            {user.name}
                        </div>
                    </div>
                </div>
                <form onSubmit={handleSubmit} style={{ padding: 32 }}>
                    <div style={{ marginBottom: 20 }}>
                        <div style={{ color: '#fff', fontWeight: 600, fontSize: 15 }}>Display Name</div>
                        <input
                            type="text"
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            style={{
                                width: '50%',
                                padding: 8,
                                borderRadius: 6,
                                border: '1px solid #36393f',
                                background: '#232428',
                                color: editMode ? '#fff' : '#b9bbbe',
                                fontSize: 15,
                                marginTop: 4,
                                pointerEvents: !editMode ? 'none' : undefined
                            }}
                        />
                    </div>
                    <div style={{ marginBottom: 20 }}>
                        <div style={{ color: '#fff', fontWeight: 600, fontSize: 15 }}>E-Mail Address</div>
                        <input
                            type="email"
                            name="email"
                            value={editMode ? formData.email : user.email}
                            onChange={handleChange}
                            readOnly={!editMode}
                            tabIndex={!editMode ? -1 : undefined}
                            style={{
                                width: '50%',
                                padding: '4px 8px',
                                borderRadius: 0,
                                border: '1px solid #36393f',
                                background: '#232428',
                                color: editMode ? '#fff' : '#b9bbbe',
                                fontSize: 15,
                                marginTop: 4,
                                pointerEvents: !editMode ? 'none' : undefined
                            }}
                        />
                    </div>
                        {!editMode ? (
                            <button
                                type="button"
                                onClick={handleEdit}
                                style={{
                                    background: '#5865f2',
                                    color: '#fff',
                                    border: 'none',
                                    borderRadius: 6,
                                    padding: '8px 20px',
                                    fontWeight: 600,
                                    cursor: 'pointer',
                                    display: 'flex',
                                }}
                            >Edit</button>
                        ) : (
                            <>
                                <div style={{ display: 'flex', gap: 8 }}>
                                    <button
                                        type="submit"
                                        disabled={loading}
                                        style={{
                                            background: '#43b581',
                                            color: '#fff',
                                            border: 'none',
                                            borderRadius: 6,
                                            padding: '8px 20px',
                                            fontWeight: 600,
                                            cursor: loading ? 'not-allowed' : 'pointer',
                                        }}
                                    >Save</button>
                                    <button
                                        type="button"
                                        onClick={handleCancel}
                                        disabled={loading}
                                        style={{
                                            background: '#f04747',
                                            color: '#fff',
                                            border: 'none',
                                            borderRadius: 6,
                                            padding: '8px 20px',
                                            fontWeight: 600,
                                            cursor: loading ? 'not-allowed' : 'pointer',
                                        }}
                                    >Cancel</button>
                                </div>
                            </>
                        )}
                </form>
            </div>
        </div>
    );
}