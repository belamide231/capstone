export class guardsDTO {

    public static authorization(token: string): { headers: { Authorization: string } } {
        return {
            headers: {
                Authorization: 'Bearer ' + token
            }
        }
    }
}




