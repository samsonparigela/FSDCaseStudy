import Delete from "./Delete";
import GetAll from "./GetAll";
import GetByID from "./GetByID";

export default function Customers(){
    return(
        <div>
            <div style={{ display: 'flex' }}>
            <Delete />
            </div>            
            <div style={{ display: 'flex' }}>
            <GetAll />
            </div>
            <div style={{ display: 'flex' }}>
            <GetByID />
            </div>
        </div>
    )
}