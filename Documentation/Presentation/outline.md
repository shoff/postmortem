# Event sourcing


## What is event sourcing

* Current state is a left fold of previous behaviours.
* A snapshot is a memoization of your left fold.
* There are a lot of domains that are naturally event sourced.

## Why use event sourcing

* By replaying events, one can get the state of a domain aggregate at any point in time.
    * There is no coupling between the representation of current state in the domain and in storage.
* Avoids impedance mismatch between the object oriented and relational world.
* As the events represent every action the system has undertaken any possible model describing the system can be built from the events.

## Where is event sourcing appropriate



## Where is event sourcing inappropriate

* Consistency, Availablility, Speed - pick two.


## Implementing event sourcing

* You can use CQRS without Event Sourcing, but with Event Sourcing you must use CQRS: [CQRS and Event Sourcing](http://youtu.be/JHGkaShoyNs)
* Events must not be removed from the system once committed. 
    * If an error is made in an event, a new event is appended to correct the mistake.
    * Just like an accountant's ledger, you are not allowed to update events.
* Periodic snapshots of an entitie's state can be saved to help with performance issues.
* Event validation **MUST** occur in the aggregate.

## CQRS

* CQRS basically says that you don’t want one system - reading and writing are different and you should make different decisions for reads and for writes.

* Every method should either be: 
    1. a command that performs an action
    2. a query that returns data to the caller
    3. **not both**
* Methods should return a value only if they are [referentially transparent](https://en.wikipedia.org/wiki/Referential_transparency) and hence possess no side effects.

* Applying CQRS on a VehicleService would result in two separate services:
    * VehicleWriteService
        * VehiclePreferredEvent
        * VehicleColorChangedEvent
        * VehicleSoldEvent
        * VehicleTotaledEvent

    * VehicleReadService
        * GetVehicleByVinEvent
        * GetVehicleByManufacturerEvent
        * GetVehicleByYearEvent

## EventStorming


## Questions

1. What and how should aggregates be created?
2. What determines when an entity is or isn't an aggregate?
    * __Almost every activity that results in the creation of an entity or storing of additional information can be traced to a transition from a previous business state. In any transition, the previous state is the aggregate root.__

## More Information

### Implementations
* [EventStore](https://eventstore.org/)
* [NEventStore](http://neventstore.org/)
* [EventSourcing Core](https://github.com/jacqueskang/EventSourcing/)
* [AxonDB](https://axoniq.io/product-overview/axondb)
* [Zatoichi Event Sourcing (nuget)](http://zatoichi.ddns.net:8080)

### Web Sites
* [CQRS](https://martinfowler.com/bliki/CQRS.html)
* [Command and Query Responsibility Segregation (CQRS) pattern](https://docs.microsoft.com/en-us/azure/architecture/patterns/cqrs)
* [Pattern: Command Query Responsibility Segregation (CQRS)](https://microservices.io/patterns/data/cqrs.html)
* [CQRS Pattern](https://medium.com/eleven-labs/cqrs-pattern-c1d6f8517314)
* [1 Year of Event Sourcing and CQRS](https://hackernoon.com/1-year-of-event-sourcing-and-cqrs-fb9033ccd1c6)
* [When to use CQRS](https://community.risingstack.com/when-to-use-cqrs/)
* [Concepts of CQRS](https://dzone.com/articles/concepts-of-cqrs)

### Pluralsight
* [Modern Software Architecture: Domain Models, CQRS, and Event Sourcing - Dino Esposito](https://app.pluralsight.com/library/courses/modern-software-architecture-domain-models-cqrs-event-sourcing/table-of-contents)
* [CQRS in Practice](https://app.pluralsight.com/library/courses/cqrs-in-practice/table-of-contents)
* [Domain-Driven Design in Practice](https://app.pluralsight.com/library/courses/domain-driven-design-in-practice/table-of-contents)

### Books
* [CQRS Documents by Greg Young](https://cqrs.files.wordpress.com/2010/11/cqrs_documents.pdf)


### Videos
* [CQRS and Event Sourcing with Jakub Pilimon](https://www.youtube.com/watch?v=rhn-T9b_Mvs)
* [Implementing event-sourced microservices](https://www.youtube.com/watch?v=HM9EgmxX0Ns&t=933s)
* [Greg Young — A Decade of DDD, CQRS, Event Sourcing ](https://www.youtube.com/watch?v=LDW0QWie21s&t=2165s)
* [Greg Young - CQRS and Event Sourcing - Code on the Beach 2014](https://www.youtube.com/watch?v=JHGkaShoyNs&t=166s)
* [Building microservices with event sourcing and CQRS ](https://www.youtube.com/watch?v=I4A5ntHeoxU)
* [CQRS and Event Sourcing with Jakub Pilimon](https://www.youtube.com/watch?v=I4A5ntHeoxU)
* [DDD, event sourcing and CQRS – theory and practice](https://www.youtube.com/watch?v=rolfJR9ERxo&t=429s)
* [2018-10 Advanced Microservices Patterns: CQRS and Event Sourcing ](https://www.youtube.com/watch?v=W_wySQ0lTI4&t=1754s)
* [DDD, event sourcing and CQRS – theory and practice](https://www.youtube.com/watch?v=rUDN40rdly8)
* [Design Patterns: Why Event Sourcing?](https://www.youtube.com/watch?v=rolfJR9ERxo&t=429s)
* [Scaling Event Sourcing for Netflix Downloads](https://www.youtube.com/watch?v=rsSld8NycCU&t=607s)
* [Event Sourcing and CQRS ](https://www.youtube.com/watch?v=0cOJwYP0rss)

