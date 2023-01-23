import {NavigationContainer} from '@react-navigation/native';
import React, {useState} from 'react';
import {View, Text, Button, StyleSheet, TextInput} from 'react-native';
import io from 'socket.io-client';
function LoginScreen({navigation}) {
  const [username, SetUsername] = useState('');
  const [password, SetPassword] = useState('');
  const [socket, setSocket] = useState(null);

  if (!socket) {
    setSocket(io('http://192.168.1.239:3000'));
  }

  return (
    <View style={styles.screen}>
      <Text>Connected to socket: {socket ? 'Yes' : 'No'}</Text>
      <TextInput
        placeholder="Email"
        style={{borderColor: 'black', borderWidth: 2, width: 100 + '%'}}
        onChangeText={username => {
          SetUsername(username);
          console.log(username);
        }}
      />
      <TextInput
        placeholder="Password"
        style={{borderColor: 'black', borderWidth: 2, width: 100 + '%'}}
        onChangeText={password => {
          SetPassword(password);
          console.log('password:' + password);
        }}
      />
      <Button
        color={'red'}
        title={'Submit'}
        onPress={() => {
          console.log('Mandato');
          socket.emit('ciao', {
            username: username,
            password: password,
          });
          socket.on('Risposta Login', messaggio => {
            if (messaggio) navigation.navigate('Main');
          });
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
