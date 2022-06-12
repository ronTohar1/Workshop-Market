enum Roles {
  Manager,
  Owner,
}
export default Roles
export function getRoleString(id:number){
  if (id == 0 ) return "Manager"
  if (id == 1) return "Owner"
  return "NO ROLE"
}