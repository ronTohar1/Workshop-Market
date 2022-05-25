
class Member {
    id: number;
    username: string;
    loggedIn: boolean;
    
  
    constructor(
      id: number,
      name: string,
      loggedIn: boolean
  
    ) {
      this.id = id;
      this.username = name;
      this.loggedIn = loggedIn;
    }
  }
  
  export default Member;
  
