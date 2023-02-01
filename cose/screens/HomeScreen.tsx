import React, {useEffect, useState} from 'react';
import Socket from '../classi/Socket';
import {Alert, Button, Text, View} from 'react-native';
import SalvataggioDati from '../classi/SalvataggioDati';

export default function HomeScreen({navigation, route}: any) {
  const [chiave, setChiave] = useState(null);
  const [socket, setSocket] = useState<Socket | null>(null);
  const [lastTockenDate, setLastTokenDate] = useState<string>('');
  async function fetchData() {
    setChiave(await SalvataggioDati.getData("@Key"));
    setLastTokenDate(await SalvataggioDati.getData("@Date"));
  }
  useEffect(() => {
    if (socket === null) {
      setSocket(new Socket('ws://192.168.1.239:4500'));
    }
    if (route.params?.chiave) {
      setChiave(route.params?.chiave);
      setLastTokenDate(route.params.data);

      SalvataggioDati.storeData(chiave,"@Key");
      SalvataggioDati.storeData(lastTockenDate,"@Date")
    }
  }, [route.params?.chiave,chiave]);
  useEffect(() => {
    if (chiave === null) fetchData();
    console.log(chiave)
  },[]);
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
          let risposto = false;

          setTimeout(() => {
            if (!risposto) {
              Alert.alert('Il server non risponde');
            }
          }, 5000);

          socket?.Ricevi('OnAutoLoginResponse', risposta => {
            risposto = true;
            if (risposta)
              navigation.navigate({
                name: 'Main',
                params: {chiave: chiave},
                merge: true,
              });
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
     
    Bisogna Salvare anche la data
      */
}
