import React from "react";

// import things related to React Navigation
import { NavigationContainer } from "@react-navigation/native";
import { createNativeStackNavigator } from "@react-navigation/native-stack";

// import screens
import LoginScreen from "./cose/screens/LoginScreen";
import MainScreen from "./cose/screens/MainScreen";


// create a "stack"
const MyStack = createNativeStackNavigator();

const App = () => {
  return (
    <NavigationContainer>
      <MyStack.Navigator>
        <MyStack.Screen name="Log-in" component={LoginScreen} />
        <MyStack.Screen name="Main" component={MainScreen} />
        </MyStack.Navigator>
    </NavigationContainer>
  );
};

export default App;