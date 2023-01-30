import React, {useEffect, useState} from 'react';
import Socket from '../classi/Socket';
import {Alert, Button, Text, View} from 'react-native';

export default function HomeScreen({navigation, route}: any) {
  const [chiave, setChiave] = useState(null);
  const [socket, setSocket] = useState<Socket | null>(null);
  const [lastTockenDate, setLastTokenDate] = useState<string>("");
  useEffect(() => {
    if (socket === null){
      setSocket(new Socket('ws://192.168.1.239:4500'))
    }
    if (route.params?.chiave) {
      setChiave(route.params.chiave);
      setLastTokenDate(route.params.data);
    }
  }, [route.params?.chiave]);

  return (
    <View>
      <Button
        color={'red'}
        title="Ho bisogno di un token"
        onPress={() => navigation.navigate('Log-in')}
      />
      <Button
        color={'blue'}
        title="Credo di avere già un token"
        onPress={() => {
          socket?.Manda('OnAutoLogin', {codice: chiave});
          socket?.Ricevi('OnAutoLoginResponse', risposta => {
            if (risposta) navigation.navigate('Main');
            else Alert.alert('Il token non è valido');
          });
        }}
      />
      <Text>Ultimo token ricevuto: {lastTockenDate}</Text>
    </View>
  );
}
{
  /* 
      async function fetchData() {
      const data = await SalvataggioDati.getData();
      setChiave(data);
    }
    fetchData();
      */
}
