import { BaseService } from "../baseService/baseService"


export class CustomersService extends BaseService {
    constructor() {
        super('/customers')
    }
}