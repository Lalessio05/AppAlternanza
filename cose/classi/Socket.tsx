export default class Socket {
  socket: WebSocket;
  constructor(indirizzo: string) {
    this.socket = new WebSocket(indirizzo);
    this.socket.onopen = () => {
      console.log('Mi sono connesso');
    };
  }

  Disconnetti() {
    this.socket.close();
  }

  Manda(nomeEvento: string, messaggio: any) {
    
    try {
      this.socket.send(
        JSON.stringify({
          nomeEvento: nomeEvento,
          username: messaggio.username,
          password: messaggio.password,
          codice: messaggio.codice,
          movimento: messaggio.movimento,
        }),
      );
    } catch (e) {
      console.log(e);
    }
  }

  Ricevi(nomeEvento: string, callback: (risposta: any) => void): any {
    this.socket.onmessage = e => {
      try {
        let risposta = JSON.parse(e.data);
        if (risposta.nomeEvento === nomeEvento ) {  //Forse dovrei fare una lista di eventi e controllare se l'evento è presente nella lista 
          callback(risposta.messaggio);
        }
      } catch (e) {
        console.log(e);
      }
    };
  }

}
