import React, {useEffect, useState} from 'react';
import {View, Button, StyleSheet, TextInput, Text} from 'react-native';
import Socket from '../classi/Socket';
import SalvataggioDati from '../classi/SalvataggioDati';
function LoginScreen({navigation}: any) {
  const [username, SetUsername] = useState('');
  const [password, SetPassword] = useState('');
  const [chiave, setChiave] = useState(new ArrayBuffer(512));
  const [socket, setSocket] = useState<Socket | null>(
    new Socket('ws://192.168.1.239:3000'),
  ); //
  // useEffect(() => {
  //   if (!socket) {
  //     const newSocket = new Socket();
  //     newSocket.SetSocket('http://192.168.1.239:3000');
  //     setSocket(newSocket);
  //   }
  //   async function fetchData() {
  //     const data = await SalvataggioDati.getData();
  //     setChiave(data);
  //   }
  //   fetchData();
  // }, []);

  return (
    <View style={styles.screen}>
      <TextInput
        placeholder="Username"
        style={{borderColor: 'black', borderWidth: 2, width: 100 + '%'}}
        onChangeText={username => {
          SetUsername(username);
        }}
      />
      <TextInput
        placeholder="Password"
        style={{borderColor: 'black', borderWidth: 2, width: 100 + '%'}}
        onChangeText={password => {
          SetPassword(password);
        }}
      />
      <Button
        color={'red'}
        title={'Submit'}
        onPress={() => {
          if (socket !== null) {
            // socket.Manda('OnSubmit', {
            //   username: username,
            //   password: password,
            // });
            socket.Manda('OnSubmit', {username: username, password: password});

            // socket.Ricevi('OnSubmitResponse', (messaggio: any) => {
            //   if (messaggio) {
            //     navigation.navigate('Main');
            //     setChiave(messaggio);
            //     SalvataggioDati.storeData(chiave);
            //   }
            // });
            socket.Ricevi('OnSubmitResponse');
          }
        }}
      />
      <Button
        color={'blue'}
        title={'Sono già loggato'}
        onPress={() => {
          if (socket !== null) {
            socket.Manda(
              'OnAutoLogin',
              "Questo è il messaggio dell'evento Pinuccio",
            );
            //   socket.Ricevi('OnAutoLoginResponse', (messaggio: any) => {
            //     if (messaggio) navigation.navigate('Main');
            //   });
            // }
            socket.Ricevi('OnAutoLoginResponse');
          }
        }}
      />
    </View>
  );
}

const styles = StyleSheet.create({
  screen: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
});

export default LoginScreen;
