import { useState, useEffect } from "react";
import axios from "axios";
import "./App.css";

function App() {
  const [tab, setTab] = useState("invited");
  const [invitations, setInvitations] = useState([]);

  useEffect(() => {
    axios.get("http://localhost:5232/api/invitations")
      .then(res => setInvitations(res.data))
      .catch(err => console.error(err));
  }, []);

  const handleAccept = async (invite) => {
    await axios.put(`http://localhost:5232/api/invitations/${invite.id}`, `"Accepted"`, {
      headers: { "Content-Type": "application/json" }
    });
    setInvitations(invitations.map(i => i.id === invite.id ? { ...i, status: "Accepted" } : i));
  };

  const handleDecline = async (invite) => {
    await axios.put(`http://localhost:5232/api/invitations/${invite.id}`, `"Declined"`, {
      headers: { "Content-Type": "application/json" }
    });
    setInvitations(invitations.map(i => i.id === invite.id ? { ...i, status: "Declined" } : i));
  };
  
  const renderCard = (invite, actions = true) => (
    <div key={invite.id} className="card">
      <div className="card-header">
        <div className="avatar">{invite.name.charAt(0)}</div>
        <div>
          <h3>{invite.name}</h3>
          <p>{invite.date}</p>
        </div>
      </div>
      <div className="details">
        📍 {invite.location} | 🧰 {invite.category} | Job ID: {invite.jobId}
      </div>
      <p className="description">{invite.description}</p>
      {invite.status === "Accepted" && (
        <div className="extra-info">
          <p>👤 Contact: {invite.full}</p>
          <p>📞 {invite.phone}</p>
          <p>✉️ {invite.email}</p>
        </div>
      )}
      <div className="card-footer">
        {actions && invite.status === "Invited" ? (
          <div style={{ display: "flex", gap: "0.5rem" }}>
            <button className="btn btn-accept" onClick={() => handleAccept(invite)}>Accept</button>
            <button className="btn btn-decline" onClick={() => handleDecline(invite)}>Decline</button>
          </div>
        ) : invite.status === "Accepted" ? (
          <span className="badge-accepted">✅ Accepted</span>
        ) : (
          <span className="badge-declined">❌ Declined</span>
        )}
        <p className="price">{invite.price} 💲 Lead Invitation</p>
      </div>
    </div>
  );

  return (
    <div className="container">
      <div className="tabs">
        <button className={`tab ${tab === "invited" ? "active" : ""}`} onClick={() => setTab("invited")}>Invited</button>
        <button className={`tab ${tab === "accepted" ? "active" : ""}`} onClick={() => setTab("accepted")}>Accepted</button>
      </div>
      {tab === "invited" && invitations.filter(i => i.status === "Invited").map(invite => renderCard(invite))}
      {tab === "accepted" && invitations.filter(i => i.status === "Accepted").map(invite => renderCard(invite, false))}
      
    </div>
  );
}

export default App;
