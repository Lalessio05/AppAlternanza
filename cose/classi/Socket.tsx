import {Alert} from 'react-native';
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
          movimento:messaggio.movimento,
        }),
      );
    } catch (e){
      console.log(e);
    }
  }

  Ricevi(nomeEvento: string, callback: (risposta: any) => void): any {
    try {
      this.socket.onmessage = e => {
        let risposta = JSON.parse(e.data);
        if (nomeEvento === 'OnSubmitResponse') {
          if (risposta.nomeEvento === nomeEvento)
            callback(risposta.messaggio);
        } else if (nomeEvento === 'OnAutoLoginResponse')
          if (risposta.nomeEvento === nomeEvento /* */)
            callback(risposta.messaggio);
      };
    } catch (e) {
      console.log(e);
    }

  }
}
