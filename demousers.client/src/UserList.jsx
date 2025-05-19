import { useState } from 'react';
export default function UserList({ users, onUserClick }) {
    const [modalOpen, setModalOpen] = useState(false);
    const [selectedUser, setSelectedUser] = useState(null);

    const openModal = (user) => {
        setSelectedUser(user);
        setModalOpen(true);
    };

    const closeModal = () => {
        setModalOpen(false);
        setSelectedUser(null);
    };

    const handleDelete = () => {
        if (selectedUser) {
            onDeleteUser(selectedUser.id);
            closeModal();
        }
    };

    return (
        <>
            <div style={{ borderRadius: 8, padding: 24 }}>
                <h2 style={{ marginBottom: 16 }}>User List</h2>
                <table style={{ width: '100%', borderCollapse: 'collapse' }}>
                    <thead>
                        <tr style={{ borderBottom: '2px solid #333' }}>
                            <th style={{ textAlign: 'left', padding: '12px 8px' }}>Name</th>
                            <th style={{ textAlign: 'left', padding: '12px 8px' }}>Actions</th>
                        </tr>
                    </thead>
                    <tbody>
                        {users.map(user => (
                            <tr key={user.id} style={{ borderBottom: '1px solid #333' }}>
                                <td style={{ padding: '12px 8px', fontWeight: 500 }}>{user.name}</td>
                                <td style={{ padding: '12px 8px' }}>
                                    <div style={{ display: "flex", gap: 8 }}>
                                    <button
                                        onClick={() => onUserClick(user.id)}
                                        style={{
                                            background: '#232428',
                                            color: '#fff',
                                            border: 'none',
                                            borderRadius: 4,
                                            padding: '6px 16px',
                                            cursor: 'pointer',
                                            fontWeight: 500
                                        }}
                                    >
                                        Details
                                    </button>
                                    <button
                                        onClick={() => openModal(user)}
                                        style={{
                                            background: '#ef4444',
                                            color: '#fff',
                                            border: 'none',
                                            borderRadius: 4,
                                            padding: '6px 16px',
                                            cursor: 'pointer',
                                            fontWeight: 500
                                        }}
                                    >
                                        Delete
                                    </button>
                                    </div>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
                {modalOpen && (
                    <div style={{
                        position: 'fixed',
                        top: 0, left: 0, right: 0, bottom: 0,
                        background: 'rgba(0,0,0,0.6)',
                        display: 'flex',
                        alignItems: 'center',
                        justifyContent: 'center',
                        zIndex: 1000
                    }}>
                        <div style={{
                            background: '#2d2d37',
                            padding: 32,
                            borderRadius: 8,
                            minWidth: 320,
                            boxShadow: '0 2px 16px rgba(0,0,0,0.3)'
                        }}>
                            <h3>Confirm Deletion</h3>
                            <p>Are you sure you want to delete <b>{selectedUser?.name}</b>?</p>
                            <div style={{ marginTop: 24, display: 'flex', gap: 12 }}>
                                <button
                                    onClick={handleDelete}
                                    style={{
                                        background: '#ef4444',
                                        color: '#fff',
                                        border: 'none',
                                        borderRadius: 4,
                                        padding: '8px 20px',
                                        cursor: 'pointer',
                                        fontWeight: 500
                                    }}
                                >
                                    Delete
                                </button>
                                <button
                                    onClick={closeModal}
                                    style={{
                                        background: '#374151',
                                        color: '#fff',
                                        border: 'none',
                                        borderRadius: 4,
                                        padding: '8px 20px',
                                        cursor: 'pointer',
                                        fontWeight: 500
                                    }}
                                >
                                    Cancel
                                </button>
                            </div>
                        </div>
                    </div>
                )}
                </div>
        </>
    );
}