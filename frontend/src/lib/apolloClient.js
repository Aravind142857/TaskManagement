import {ApolloClient, ApolloLink, InMemoryCache, HttpLink, from} from "@apollo/client"

const authLink = new ApolloLink((operation, forward) => {
    const token = localStorage.getItem("authToken");
    operation.setContext({
        headers: {
            Authorization: token ? `Bearer ${token}`: "",
        },
    });
    return forward(operation);
});
const httpLink = new HttpLink({ uri: "http://localhost:5001/graphql" });

const client = new ApolloClient({
    cache: new InMemoryCache(),
    link: from([authLink, httpLink]),
    // uri: "http://localhost:5001/graphql",
    // defaultOptions: {
    //     watchQuery: {
    //         errorPolicy: "all",
    //     },
    //     query: {
    //         errorPolicy: "all",
    //     },
    // },
});
export default client;