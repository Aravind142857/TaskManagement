import {ApolloClient, gql} from "@apollo/client";
import client from "@/lib/apolloClient";

export const REGISTER_USER = gql`
    mutation RegisterUser($input: UserRegisterInput!) {
        register(input: $input) {
            token,
            userId
        }
    }
`;
export const LOGIN_USER = gql`
    mutation LoginUser($email: String!, $password: String!) {
        login(input: {
            email: $email,
            password: $password
        })
        {
            token
            userId
        }
    }
`;

interface UserAuthResponse {
    login: {
        "token": string,
        "userId": string
    };
}

export async function registerUser(username: string, email: string, password: string): Promise<void> {
    const response = await client.mutate({
        mutation: REGISTER_USER,
        variables: {
            input: {
                username,
                email,
                password
            }
        }
    });
    if (response.errors) {
        throw new Error("Registration failed");
    }
};

export async function loginUser(email: string, password: string): Promise<[string, string]> {
    const response = await client.mutate<UserAuthResponse>({
        mutation: LOGIN_USER,
        variables: { email, password },
    });
    const res = response.data?.login;
    if (!res) {
        throw new Error("login failed");
    }
    const token: string = res.token;
    localStorage.setItem("authToken", token);
    const userId: string = res.userId;
    localStorage.setItem("userId", userId);
    return [token, userId];
};

export function logout(): void {
    localStorage.removeItem("authToken");
    localStorage.removeItem("userId");
    window.location.reload();
}